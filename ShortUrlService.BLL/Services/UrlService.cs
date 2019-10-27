using ShortUrlService.BLL.AutoMapper;
using ShortUrlService.BLL.Models;
using ShortUrlService.DAL.Models;
using ShortUrlService.DAL.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrlService.BLL.Services
{
    public class UrlService
    {
        private IUnitOfWork  db;

        public UrlService()
        {
            db = new UnitOfWork();
        }

        public List<UrlDTO> GetAllUrls()
        {
            var mapper = AutoMapperConfigs.UrlEntryToUrlDTO.CreateMapper();   

            var urlEntrys = db.Urls.GetAll();

            var urlDtos = mapper.Map<List<UrlDTO>>(urlEntrys);

            return urlDtos;
        }

        public void DeleteUrl(long id)
        {
            db.Urls.Delete(id);
            db.Save();
        }

        public UrlDTO GetUrl(long id)
        {
            var url = db.Urls.Get(id);

            if (url == null)
            {
                throw new Exception(string.Format($"Не удалось получить url из базы по id {id}"));
            }

            var mapper = AutoMapperConfigs.UrlEntryToUrlDTO.CreateMapper();

            return mapper.Map<UrlDTO>(url);
        }

        public void PlusRedirectionByHashCode(int hashCode)
        {
            var url = GetUrlEntryByHashCode(hashCode);

            if (url == null)
            {
                throw new Exception(string.Format($"Не удалось получить url из базы по по хэш коду полного url {hashCode}"));
            }

            var mapper = AutoMapperConfigs.UrlEntryToUrlDTO.CreateMapper();
            var urlDto = mapper.Map<UrlDTO>(url);

            if (urlDto != null)
            {
                urlDto.RedirectionCount++;

                CreateOrUpdate(urlDto);
            }
        }

        public string GetFullUrlByHashCode(int hashCode)
        {
            var url = GetUrlEntryByHashCode(hashCode);

            if (url == null)
            {
                throw new Exception(string.Format($"Не удалось получить url из базы по по хэш коду полного url {hashCode}"));
            }

            return url.FullUrl;
        }

        private UrlEntry GetUrlEntryByHashCode(int hashCode)
        {
            return db.Urls.Find(u => u.UrlHashCode == hashCode).FirstOrDefault();
        }

        public void CreateOrUpdate(UrlDTO url)
        {
            var mapper = AutoMapperConfigs.UrlDTOToUrlEntry.CreateMapper();

            var urlEntry = db.Urls.Get(url.Id);

            if (urlEntry != null)
            {
                urlEntry.FullUrl = url.FullUrl;

                urlEntry.RedirectionCount = url.RedirectionCount;

                GenerateShortUrl(urlEntry);

                db.Urls.Update(urlEntry);
            }
            else
            {
                urlEntry = mapper.Map<UrlEntry>(url);
                GenerateShortUrl(urlEntry);
                db.Urls.Create(urlEntry);
            }

            db.Save();
        }

        private void GenerateShortUrl(UrlEntry urlEntry)
        {
            var fullUrlHashCode = urlEntry.FullUrl.GetHashCode();
            urlEntry.UrlHashCode = fullUrlHashCode;
            urlEntry.ShortUrl = string.Format($"/?id={fullUrlHashCode}");
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
