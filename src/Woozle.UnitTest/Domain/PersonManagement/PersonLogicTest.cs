using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Woozle.Domain.PersonManagement;
using Woozle.Model;
using Woozle.Model.SessionHandling;
using Woozle.Persistence.Repository;

namespace Woozle.Core.BusinessLogic.Impl.Test.PersonManagement
{
    [TestClass]
    public class PersonLogicTest
    {
        private Person person;
        private IPersonLogic logic;
        private Session session;
        private Mock<IRepository<Person>> personRepositoryMock;
        private IQueryable<Person> personData;

        [TestInitialize]
        public void Initialize()
        {
            personRepositoryMock = new Mock<IRepository<Person>>(MockBehavior.Strict);
            
            session = new Session(Guid.NewGuid(), null, DateTime.Now);
            person = new Person();
            logic = new PersonLogic(personRepositoryMock.Object);

            personData = new List<Person>()
                             {
                                 new Person()
                                     {
                                         Id = 1,
                                         FirstName = "Andreas",
                                         LastName = "Schürmann",
                                         Street = "Geissburghalde 17",
                                         CityId = 1
                                     },
                                 new Person()
                                     {
                                         Id = 2,
                                         FirstName = "Andreas",
                                         LastName = "Schürmann",
                                         Street = "Geissburghalde 17",
                                         CityId = 2
                                     },
                                 new Person()
                                     {
                                         Id = 3,
                                         EnterpriseName = "Bison Schweiz AG",
                                         FirstName = "Andreas",
                                         LastName = "Schürmann",
                                         Street = "Allee 1a",
                                         CityId = 55
                                     },
                                 new Person()
                                     {
                                         Id = 4,
                                         FirstName = "Patrick",
                                         LastName = "Roos",
                                         Street = "Geissburghalde 17",
                                         CityId = 1
                                     },
                                      new Person()
                                     {
                                         Id = 6,
                                         EnterpriseName = "Bison Schweiz AG",
                                         Street = "Allee 1a",
                                         CityId = 55
                                     }
                             }.AsQueryable();
        }

        [TestMethod]
        public void CheckForExistingPersonWithExistingEnterpriseTest()
        {
            person.EnterpriseName = "Bison Schweiz AG";
            person.Street = "Allee 1a";
            person.CityId = 55;

            personRepositoryMock.Setup(n => n.CreateQueryable(session)).Returns(personData);

            var result = logic.SearchForExistingPerson(person, session);

            Assert.AreEqual(6, result.Id);
            Assert.AreEqual(PState.Modified, result.PersistanceState);
        }

        [TestMethod]
        public void CheckForExistingPersonWithNotExistingEnterpriseTest()
        {
            person.EnterpriseName = "Basenet Informatik AG";
            person.Street = "Wassergrabe 1";
            person.CityId = 55;

            personRepositoryMock.Setup(n => n.CreateQueryable(session)).Returns(personData);

            var result = logic.SearchForExistingPerson(person, session);

            Assert.AreEqual(0, result.Id);
            Assert.AreEqual(PState.Added, result.PersistanceState);
        }

        [TestMethod]
        public void CheckForExistingPersonWithNotExistingPersonTest()
        {
            person.FirstName = "Susi";
            person.LastName = "Sorglos";
            person.EMail = "Test";
            person.Phone = "Phone";
            person.Mobile = "Mobile";
            person.CityId = 55;

            personRepositoryMock.Setup(n => n.CreateQueryable(session)).Returns(personData);

            var result = logic.SearchForExistingPerson(person, session);

            Assert.AreEqual(0, result.Id);
            Assert.AreEqual(PState.Added, result.PersistanceState);
            Assert.AreEqual(person.EMail, result.EMail);
            Assert.AreEqual(person.Phone, result.Phone);
            Assert.AreEqual(person.Mobile, result.Mobile);
        }

        [TestMethod]
        public void CheckForExistingPersonWithNotExistingPersonButSameNameTest()
        {
            person.FirstName = "Andreas";
            person.LastName = "Schürmann";
            person.Street = "DieAndereStrasse 1";
            person.CityId = 55;

            personRepositoryMock.Setup(n => n.CreateQueryable(session)).Returns(personData);

            var result = logic.SearchForExistingPerson(person, session);

            Assert.AreEqual(0, result.Id);
            Assert.AreEqual(PState.Added, result.PersistanceState);
        }


        [TestMethod]
        public void CheckForExistingPersonWithExistingPersonTest()
        {
            person.FirstName = "Andreas";
            person.LastName = "Schürmann";
            person.Street = "Geissburghalde 17";
            person.CityId = 1;

            personRepositoryMock.Setup(n => n.CreateQueryable(session)).Returns(personData);

            var result = logic.SearchForExistingPerson(person, session);

            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(PState.Modified, result.PersistanceState);
        }

        [TestMethod]
        public void CheckForExistingPersonWithUpdatedPersonTest()
        {
            person.Id = 55;
            person.FirstName = "Andreas";
            person.LastName = "Schürmann";
            person.Street = "Geissburghalde 17";
            person.CityId = 1;

            personRepositoryMock.Setup(n => n.CreateQueryable(session)).Returns(personData);

            var result = logic.SearchForExistingPerson(person, session);

            Assert.AreEqual(55, result.Id);
        }
    }
}
