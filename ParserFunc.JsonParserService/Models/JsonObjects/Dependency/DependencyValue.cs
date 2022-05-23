using System.Collections.Generic;

namespace ParserFunc.JsonParserService.Models.JsonObjects.Dependency
{
    public class DependencyValue
    {
        public string OId { get; set; }

        public List<ApplicationType> ApplicationTypes { get; set; }
    }
}