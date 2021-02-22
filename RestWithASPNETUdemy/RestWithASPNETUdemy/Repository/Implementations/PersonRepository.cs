using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Repository.Implementations
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MysqlContext context) : base(context) { }
        public Person Disable(long id)
        {
            if (!_mysqlContext.People.Any(p => p.Id.Equals(id))) return null;

            var person = _mysqlContext.People.SingleOrDefault(p => p.Id.Equals(id));
            if (person != null)
            {
                person.Enabled = false;

                try
                {
                    _mysqlContext.Entry(person).CurrentValues.SetValues(person);
                    _mysqlContext.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return person;
        }

        public List<Person> FindByName(string firstName, string secondName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(secondName))
            {
                return _mysqlContext.People.Where(p =>
                                    p.FirstName.Contains(firstName)
                                    && p.LastName.Contains(secondName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(secondName))
            {
                return _mysqlContext.People.Where(p =>
                                    p.FirstName.Contains(firstName)).ToList();
            }
            else if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(secondName))
            {
                return _mysqlContext.People.Where(p =>
                                   p.LastName.Contains(secondName)).ToList();
            }

            return null;
        }
    }
}
