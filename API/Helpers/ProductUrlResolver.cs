using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
   //ovako omogućujemo da nam bude vidljiv cijeli path do slike koju šaljemo klijentu
   //u postmanu ti je sada to vidljivo nakon što i u automapperu napraviš izmjene
   //mora biti string jer je pictureurl
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        //ovo je interfejs koji smo extract
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                //apiurl je adresa iz appsetings.development.json
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}