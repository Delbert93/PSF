using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreLogin.Shared;

namespace TheStoreLoginTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValidateUserNameOnLoginOrRegistration()
        {
            string badLengthUserName = "hellow";
            string badCharUserName = "hellooooow!";
            string badEmptyUserName = "";
            string badNullUserName = null;
            string goodUserName = "mckinnin";
            string goodPassword = "abcdefghijklmnopqrstuvwxyz";

            //do a try catch here
            //UserModel userModelFailNull = new UserModel(badNullUserName, goodPassword);


            TaintedUserModel taintedUserBadEmpty = new TaintedUserModel();
            taintedUserBadEmpty.Username = badEmptyUserName;
            taintedUserBadEmpty.Password = goodPassword;
            UserModel userModelFailEmpty = new UserBuilder()
                .UseName(taintedUserBadEmpty.Username)
                .UsePassword(taintedUserBadEmpty.Password)
                .Build();

            Assert.IsFalse(userModelFailEmpty.isValidUser);


            TaintedUserModel taintedUserBadLength = new TaintedUserModel();
            taintedUserBadLength.Username = badLengthUserName;
            taintedUserBadLength.Password = goodPassword;
            UserModel userModelFailLength = new UserBuilder()
                .UseName(taintedUserBadLength.Username)
                .UsePassword(taintedUserBadLength.Password)
                .Build();

            Assert.IsFalse(userModelFailLength.isValidUser);


            TaintedUserModel taintedUserBadChar = new TaintedUserModel();
            taintedUserBadChar.Username = badCharUserName;
            taintedUserBadChar.Password = goodPassword;
            UserModel userModelFailChar = new UserBuilder()
                .UseName(taintedUserBadChar.Username)
                .UsePassword(taintedUserBadChar.Password)
                .Build();

            Assert.IsFalse(userModelFailChar.isValidUser);


            TaintedUserModel taintedUserGood = new TaintedUserModel();
            taintedUserGood.Username = goodUserName;
            taintedUserGood.Password = goodPassword;
            UserModel userModelPass = new UserBuilder()
                .UseName(taintedUserGood.Username)
                .UsePassword(taintedUserGood.Password)
                .Build();

            Assert.IsTrue(userModelPass.isValidUser);
        }

        [TestMethod]
        public void ValidatePasswordOnLoginOrRegistration()
        {
            string passwordThatIsToShort = "hellow";
            string passwordWithInvalidChar = "helloooooooooooooooo*";
            string passwordThatIsEmpty = "";
            string passwordThatisNull = null;
            string goodPassword = "mckinninLloyd12";
            string goodUsername = "mckinnin";


            //do a try catch here
            //UserModel userModelFailNull = new UserModel(goodUsername, passwordThatisNull);


            TaintedUserModel taintedUserBadEmpty = new TaintedUserModel();
            taintedUserBadEmpty.Username = goodUsername;
            taintedUserBadEmpty.Password = passwordThatIsEmpty;
            UserModel userModelFailEmpty = new UserBuilder()
                .UseName(taintedUserBadEmpty.Username)
                .UsePassword(taintedUserBadEmpty.Password)
                .Build();

            Assert.IsFalse(userModelFailEmpty.isValidUser);

            TaintedUserModel taintedUserBadLength = new TaintedUserModel();
            taintedUserBadLength.Username = goodUsername;
            taintedUserBadLength.Password = passwordThatIsToShort;
            UserModel userModelFailLength = new UserBuilder()
                .UseName(taintedUserBadLength.Username)
                .UsePassword(taintedUserBadLength.Password)
                .Build();

            Assert.IsFalse(userModelFailLength.isValidUser);


            TaintedUserModel taintedUserBadChar = new TaintedUserModel();
            taintedUserBadChar.Username = goodUsername;
            taintedUserBadChar.Password = passwordWithInvalidChar;
            UserModel userModelFailChar = new UserBuilder()
                .UseName(taintedUserBadChar.Username)
                .UsePassword(taintedUserBadChar.Password)
                .Build();

            Assert.IsFalse(userModelFailChar.isValidUser);

            TaintedUserModel taintedUserGood = new TaintedUserModel();
            taintedUserGood.Username = goodUsername;
            taintedUserGood.Password = goodPassword;
            UserModel userModelPass = new UserBuilder()
                .UseName(taintedUserGood.Username)
                .UsePassword(taintedUserGood.Password)
                .Build();

            Assert.IsTrue(userModelPass.isValidUser);
        }

        [TestMethod]
        public void CheckReadOnceLogicOnPassword()
        {
            string goodPassword = "mckinninLloyd12";
            string goodUsername = "mckinnin";
            string goodEmail = "mk.rigoli@gmail.com";

            TaintedUserModel taintedUserGood = new TaintedUserModel();
            taintedUserGood.Username = goodUsername;
            taintedUserGood.Password = goodPassword;
            taintedUserGood.Email = goodEmail;
            UserModel userModelPass = new UserBuilder()
                .UseName(taintedUserGood.Username)
                .UsePassword(taintedUserGood.Password)
                .UseEmail(taintedUserGood.Email)
                .Build();

            Assert.IsTrue(userModelPass.isValidUser);
            Assert.IsFalse(userModelPass.readFlag);

            userModelPass.getPassword();
            string nullString = userModelPass.getPassword();

            Assert.IsNull(nullString);
            Assert.IsTrue(userModelPass.readFlag);
        }
    }
}
