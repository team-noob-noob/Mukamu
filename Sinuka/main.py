from Core.Factories.UserFactory import UserFactory
from Core.Repositories.UserRepository import UserRepository
from Core.Models.User import User

factory = UserFactory()
user = factory.CreateUser("test", "test", "test")

repo = UserRepository()
repo.AddUser(user)

