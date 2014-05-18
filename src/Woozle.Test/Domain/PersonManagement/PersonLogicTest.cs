using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Woozle.Domain.PersonManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence;
using Xunit;

namespace Woozle.Test.Domain.PersonManagement
{
    public class PersonLogicTest
    {
        private Person person;
        private IPersonLogic logic;
        private Session session;
        private Mock<IRepository<Person>> personRepositoryMock;
        private IQueryable<Person> personData;

        public PersonLogicTest()
        {
            personRepositoryMock = new Mock<IRepository<Person>>(MockBehavior.Strict);

            session = new Session(null);
            person = new Person();
            logic = new PersonLogic(personRepositoryMock.Object);

            personData = new List<Person>()
                         {
                             new Person()
                             {
                                 Id = 1,
                                 FirstName = "Firstname 1",
                                 LastName = "Lastname 1",
                                 Street = "Street 1",
                                 CityId = 1
                             },
                             new Person()
                             {
                                 Id = 2,
                                 FirstName = "Firstname 2",
                                 LastName = "Lastname 2",
                                 Street = "Street 2",
                                 CityId = 2
                             },
                             new Person()
                             {
                                 Id = 3,
                                 EnterpriseName = "Enterprise 2",
                                 FirstName = "Firstname 1",
                                 LastName = "Lastname 1",
                                 Street = "Street 2",
                                 CityId = 55
                             },
                             new Person()
                             {
                                 Id = 4,
                                 FirstName = "Firstname 2",
                                 LastName = "Lastname 2",
                                 Street = "Street 2",
                                 CityId = 1
                             },
                             new Person()
                             {
                                 Id = 6,
                                 EnterpriseName = "Enterprise 1",
                                 Street = "Street 1",
                                 CityId = 55
                             }
                         }.AsQueryable();
        }



        [Fact]
        public void CheckForExistingPersonWithExistingEnterpriseTest()
        {
            person.EnterpriseName = "Enterprise 1";
            person.Street = "Street 1";
            person.CityId = 55;

            personRepositoryMock.Setup(n => n.CreateQueryable(session.SessionData)).Returns(personData);

            var result = logic.SearchForExistingPerson(person, session.SessionData);

            Assert.Equal(6, result.Id);
            Assert.Equal(PState.Modified, result.PersistanceState);
        }

        [Fact]
        public void CheckForExistingPersonWithNotExistingEnterpriseTest()
        {
            person.EnterpriseName = "Enterprise 2";
            person.Street = "Street 2";
            person.CityId = 55;

            personRepositoryMock.Setup(n => n.CreateQueryable(session.SessionData)).Returns(personData);

            var result = logic.SearchForExistingPerson(person, session.SessionData);

            Assert.Equal(0, result.Id);
            Assert.Equal(PState.Added, result.PersistanceState);
        }

        [Fact]
        public void CheckForExistingPersonWithNotExistingPersonTest()
        {
            person.FirstName = "Susi";
            person.LastName = "Sorglos";
            person.EMail = "Test";
            person.Phone = "Phone";
            person.Mobile = "Mobile";
            person.CityId = 55;

            personRepositoryMock.Setup(n => n.CreateQueryable(session.SessionData)).Returns(personData);

            var result = logic.SearchForExistingPerson(person, session.SessionData);

            Assert.Equal(0, result.Id);
            Assert.Equal(PState.Added, result.PersistanceState);
            Assert.Equal(person.EMail, result.EMail);
            Assert.Equal(person.Phone, result.Phone);
            Assert.Equal(person.Mobile, result.Mobile);
        }

        [Fact]
        public void CheckForExistingPersonWithNotExistingPersonButSameNameTest()
        {
            person.FirstName = "Person 123";
            person.LastName = "Person 123";
            person.Street = "DieAndereStrasse 1";
            person.CityId = 55;

            personRepositoryMock.Setup(n => n.CreateQueryable(session.SessionData)).Returns(personData);

            var result = logic.SearchForExistingPerson(person, session.SessionData);

            Assert.Equal(0, result.Id);
            Assert.Equal(PState.Added, result.PersistanceState);
        }


        [Fact]
        public void CheckForExistingPersonWithExistingPersonTest()
        {
            person.FirstName = "Firstname 1";
            person.LastName = "Lastname 1";
            person.Street = "Street 1";
            person.CityId = 1;

            personRepositoryMock.Setup(n => n.CreateQueryable(session.SessionData)).Returns(personData);

            var result = logic.SearchForExistingPerson(person, session.SessionData);

            Assert.Equal(1, result.Id);
            Assert.Equal(PState.Modified, result.PersistanceState);
        }

        [Fact]
        public void CheckForExistingPersonWithUpdatedPersonTest()
        {
            person.Id = 55;
            person.FirstName = "Firstname 1";
            person.LastName = "Lastname 1";
            person.Street = "Street 1";
            person.CityId = 1;

            personRepositoryMock.Setup(n => n.CreateQueryable(session.SessionData)).Returns(personData);

            var result = logic.SearchForExistingPerson(person, session.SessionData);

            Assert.Equal(55, result.Id);
        }
    }
}
