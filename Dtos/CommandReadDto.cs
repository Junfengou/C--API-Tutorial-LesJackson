using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{
    /*
       The purpose of Data Transfer Object [Dto] is to encapsulate object properties and hide fields so that users on the frontend can't access
   */

    // Since this Dto is read only, we don't need data annotation to specify what requirement each field needs
    // Once all the fields are identified, now we need to map everything together by using auto mapper


    public class CommandReadDto
    {

        public int Id { get; set; }

        public string HowTo { get; set; }

        public string Line { get; set; }

        // public string Platform { get; set; }
    }
}