﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Woozle.Core.BusinessLogic.Authority;
using Woozle.Core.BusinessLogic.Impl.Authority;
using Woozle.Core.Model;
using Woozle.Core.Model.SessionHandling;
using Woozle.Core.Persistence.Repository;
using Woozle.Model;

namespace Woozle.Core.BusinessLogic.Impl.Test.Authority
{
    [TestClass]
    public class GetRolesLogicTest
    {
        private IGetRolesLogic getRolesLogic;
        private Mock<IRepository<MandatorRole>> mandatorRoleRepositoryMock;
        private IQueryable<MandatorRole> mandatorRoles;
        private Mandator mandator;
        private SessionData sessionObject;
        private Session session;

        [TestInitialize]
        public void Initialize()
        {
            this.mandatorRoleRepositoryMock = new Mock<IRepository<MandatorRole>>();
            this.getRolesLogic = new GetRolesLogic(mandatorRoleRepositoryMock.Object);

            mandator = new Mandator();
            sessionObject = new SessionData(null, mandator);
            session = new Session(Guid.NewGuid(), sessionObject, DateTime.Now.AddDays(1));

            mandatorRoles = new List<MandatorRole>()
                                {
                                    new MandatorRole()
                                        {
                                            MandId = 1,
                                            Mandator = new Mandator() {Id = 1, MandatorGroupId = 1},
                                            Role = new Role() {Id = 1}
                                        },
                                    new MandatorRole()
                                        {
                                            MandId = 2,
                                            Mandator = new Mandator() {Id = 2, MandatorGroupId = 1},
                                            Role = new Role() {Id = 1}
                                        },
                                    new MandatorRole()
                                        {
                                            MandId = 3,
                                            Mandator = new Mandator() {Id = 3},
                                            Role = new Role() {Id = 1}
                                        }
                                }.AsQueryable();
        }


        [TestMethod]
        public void GetAllMandatorRolesByMandatorGroupTest()
        {
            session.SessionObject.Mandator.Id = 1;
            session.SessionObject.Mandator.MandatorGroupId = 1;

            this.mandatorRoleRepositoryMock.Setup(n => n.CreateQueryable(It.IsAny<Session>()))
                .Returns(mandatorRoles);

            var result = getRolesLogic.GetAllMandatorRolesByMandator(session);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].MandId);
            Assert.IsNotNull(result[0].Mandator);
            Assert.IsNotNull(result[0].Role);
            Assert.AreEqual(2, result[1].MandId);
            Assert.IsNotNull(result[1].Mandator);
            Assert.IsNotNull(result[1].Role);
        }

        [TestMethod]
        public void GetAllMandatorRolesByMandatorWithoutGroupTest()
        {
            session.SessionObject.Mandator.Id = 3;

            this.mandatorRoleRepositoryMock.Setup(n => n.CreateQueryable(It.IsAny<Session>()))
                .Returns(mandatorRoles);

            var result = getRolesLogic.GetAllMandatorRolesByMandator(session);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(3, result[0].MandId);
            Assert.IsNotNull(result[0].Mandator);
            Assert.IsNotNull(result[0].Role);
        }

    }
}