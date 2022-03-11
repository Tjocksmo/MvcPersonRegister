using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPersonRegister.Models.DataTransferObjects
{
    public class PersonDto //: IValidatableObject
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^[A-Za-zåäöÅÄÖ]+$")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^[A-Za-zåäöÅÄÖ]+$")]
        public string SecondName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [RegularExpression("^[0-9]{8}-[0-9]{4}$", ErrorMessage = "Invalid social security number.")]
        public string SocialSecurityNr { get; set; }

        public static PersonDto FromDomain(Person person)
        {
            return new PersonDto()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                SecondName = person.SecondName,
                Email = person.Email,
                SocialSecurityNr = person.SocialSecNr
            };
        }
        public Person ToDomain()
        {
            return new Person
            {
                Id = this.Id,
                FirstName = this.FirstName,
                SecondName = this.SecondName,
                Email = this.Email,
                SocialSecNr = this.SocialSecurityNr
            };
        }
    }
}
