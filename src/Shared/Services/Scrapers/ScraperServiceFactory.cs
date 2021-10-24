using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Services.Scrapers
{
    public class ScraperServiceFactory
    {

        public string ScraperProvider { get; set; }

        public IScraperService Build()
        {
            IEnumerable<Type> scraperServiceTypes = System.Reflection.Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(
                    mytype => mytype.GetInterfaces().Contains(typeof(IScraperService))
                );
            
            // Over-Complicate getting the scraper provider
            // if this.ScraperProvider equals to any of the 
            // IScraperServices Implementations' GetProviderName method
            // returns the instance of the matched IScraperServices 
            // the downside of this, all of the implementations will be instantiated
            // until the correct provider is found
            // the benefit of this approach is that we don't have to touch this class again
            // everytime we add new IScraperService's implementation
            foreach (Type scraperServiceType in scraperServiceTypes)
            {
                ConstructorInfo magicConstructor = scraperServiceType.GetConstructor(Type.EmptyTypes);
                object magicClassObject = magicConstructor.Invoke(new object[]{});
                MethodInfo magicMethod = scraperServiceType.GetMethod("GetProviderName");
                string providerName = (string) magicMethod.Invoke(magicClassObject, null);

                // return the matching implementation
                if (this.ScraperProvider.ToLower() == providerName) {
                    return (IScraperService) magicClassObject;
                }
            }

            throw new ArgumentException($"Invalid Scraper Provider: {this.ScraperProvider}");
        }
    }
}