from Core.Factories.UserFactory import UserFactory
test = UserFactory()

user = test.CreateUser("test", "test", "test")
print(user.hashedPassword)

