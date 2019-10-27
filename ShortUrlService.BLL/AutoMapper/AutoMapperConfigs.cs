using AutoMapper;
using ShortUrlService.BLL.Models;
using ShortUrlService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrlService.BLL.AutoMapper
{
    class AutoMapperConfigs
    {
        private static MapperConfiguration urlDTOToUrlEntry;
        private static MapperConfiguration urlEntryToUrlDTO;
        public static MapperConfiguration UrlDTOToUrlEntry
        {
            get
            {
                if (urlDTOToUrlEntry == null)
                {
                    urlDTOToUrlEntry = new MapperConfiguration(cfg => cfg.CreateMap<UrlDTO, UrlEntry>());
                }
                return urlDTOToUrlEntry;
            }
        }

        public static MapperConfiguration UrlEntryToUrlDTO
        {
            get
            {
                if (urlEntryToUrlDTO == null)
                {
                    urlEntryToUrlDTO = new MapperConfiguration(cfg => cfg.CreateMap<UrlEntry, UrlDTO> ());
                }
                return urlEntryToUrlDTO;
            }
        }

    }
}
