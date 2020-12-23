using System;

namespace BookStore.Models.DtoModels
{
    public class BookDto: BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsDisabled { get; set; }
    }
}
