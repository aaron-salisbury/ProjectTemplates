using DotNetFramework.Data.Entities;

namespace DotNetFramework.Business.Modules.Sample.DTOs
{
    public class FlatColorDto
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _hex;
        public string Hex
        {
            get { return _hex; }
            set { _hex = value; }
        }

        internal static FlatColorDto MapToDto(FlatColor entity)
        {
            return new FlatColorDto()
            {
                Name = entity.Name,
                Hex = entity.Hex
            };
        }
    }
}
