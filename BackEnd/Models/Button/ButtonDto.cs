
using ChatsConstructor.WebApi.Models.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChatsConstructor.WebApi.Dto
{
    public class ButtonDto
    {
        [Required]
        public string Text { get; set; }
        [Required]
        //Как выглядеть будет выбор из enum? Раскрывающийся список 1. Красный 2. Синий 3. Зеленый ? Если так, то и проверка не нужна эта:
        [EnumDataType(typeof(QuestionType), ErrorMessage = "Цвет кнопки может быть красной, синей или зеленой")]
        public ColorType ColorType { get; set; }
    }
}