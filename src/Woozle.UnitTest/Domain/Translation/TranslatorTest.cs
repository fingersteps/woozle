//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Woozle.Core.BusinessLogic.Impl.Translate;
//using Woozle.Core.Model;
//using Woozle.Core.Model.SessionHandling;
//using Woozle.Core.Persistence.Repository;

//namespace Woozle.Core.BusinessLogic.Impl.Test.Translate
//{
//    [TestClass]
//    public class TranslatorTest
//    {
//        private Translator translator;
//        private Mock<IRepository<Message>> repositoryMock;
//        private IQueryable<Message> messages;

//        [TestInitialize]
//        public void Initialize()
//        {
//            repositoryMock = new Mock<IRepository<Message>>();
//            this.translator = new Translator(repositoryMock.Object);

//            messages = new List<Message>
//                           {
//                               new Message()
//                                   {
//                                       Code = "Message1",
//                                       Translation = new Translation
//                                                         {
//                                                             Id = 1,
//                                                             DefaultDescription = "Test 1",
//                                                             TranslationItems =
//                                                                 new FixupCollection<TranslationItem>()
//                                                                     {
//                                                                         new TranslationItem
//                                                                             {
//                                                                                 Id = 1,
//                                                                                 LanguageId = 1,
//                                                                                 Description = "adfjlasfd"
//                                                                             },
//                                                                         new TranslationItem
//                                                                             {
//                                                                                 Id = 2,
//                                                                                 LanguageId = 2,
//                                                                                 Description = "ssssss"
//                                                                             }
//                                                                     }
//                                                         }
//                                   },


//                               new Message()
//                                   {
//                                       Code = "Message2",
//                                       Translation =
//                                           new Translation
//                                               {
//                                                   Id = 2,
//                                                   DefaultDescription = "Übersetz mich!",
//                                                   TranslationItems = new FixupCollection<TranslationItem>()
//                                                                          {
//                                                                              new TranslationItem
//                                                                                  {
//                                                                                      Id = 1,
//                                                                                      LanguageId = 1,
//                                                                                      Description =
//                                                                                          "Übersetz mich!"
//                                                                                  },
//                                                                              new TranslationItem
//                                                                                  {
//                                                                                      Id = 2,
//                                                                                      LanguageId = 2,
//                                                                                      Description =
//                                                                                          "Translate me!"
//                                                                                  }
//                                                                          }
//                                               }
//                                   },

//                               new Message()
//                                   {
//                                       Code = "Message3",
//                                       Translation =
//                                           new Translation
//                                               {
//                                                   Id = 3,
//                                                   DefaultDescription = "Warum nur?",
//                                                   TranslationItems = new FixupCollection<TranslationItem>()
//                                               }
//                                   }

//                           }.AsQueryable();
//        }

//        [TestMethod]
//        public void GetTranslatedTextTest()
//        {
//            repositoryMock.Setup(n => n.CreateQueryable(It.IsAny<Session>())).Returns(messages);

//            var text = this.translator.GetTranslatedText(new Session(Guid.NewGuid(), new SessionData(new User
//                                                                                                         {
//                                                                                                             LanguageId
//                                                                                                                 = 2
//                                                                                                         },
//                                                                                                     new Mandator()),
//                                                                     DateTime.Now), "Message2");

//            Assert.AreEqual("Translate me!", text);
//        }

//        [TestMethod]
//        public void GetTranslatedTextTranslationNotFoundTest()
//        {

//            repositoryMock.Setup(n => n.CreateQueryable(It.IsAny<Session>())).Returns(messages);

//            var text = this.translator.GetTranslatedText(new Session(Guid.NewGuid(), new SessionData(new User
//                                                                                                         {
//                                                                                                             LanguageId
//                                                                                                                 = 2
//                                                                                                         },
//                                                                                                     new Mandator()),
//                                                                     DateTime.Now), "Message3");

//            Assert.AreEqual("Warum nur?", text);
//        }
//    }
//}
