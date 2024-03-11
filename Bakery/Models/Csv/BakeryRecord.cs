using CsvHelper.Configuration.Attributes;

namespace Bakery.Models.Csv

{
    public class BakeryRecord
    {
        [Name("Id")]
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
}
