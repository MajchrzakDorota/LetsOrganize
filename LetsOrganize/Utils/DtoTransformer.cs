using LetsOrganize.Entities;
using LetsOrganize.Models;

namespace LetsOrganize.Utils
{
    public static class DtoTransformer
    {
        public static ElementDto CreateElementDto(Element element)
        {
            return new ElementDto
            {
                Id = element.Id,
                Name = element.Name,
                Quantity = element.Quantity,
                Unit = element.Unit,
                MyListId = element.MyListId
            };
        }

        public static Element CreateElementFromElementDto(ElementDto elementDto)
        {
            return new Element
            {
                Id = elementDto.Id,
                Name = elementDto.Name,
                Unit = elementDto.Unit,
                Quantity = elementDto.Quantity,
                MyListId = elementDto.MyListId
            };
        }

        public static MyList CreateMyListFromMyListDto(MyListDto myListDto)
        {
            return new MyList
            {
                Id = myListDto.Id,
                Name = myListDto.Name
            };
        }

        public static MyListDto CreateMyListDto(MyList myList)
        {
            return new MyListDto
            {
                Id = myList.Id,
                Name = myList.Name
            };
        }

    }
}
