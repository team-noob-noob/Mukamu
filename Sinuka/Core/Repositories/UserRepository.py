from ..Factories.UserFactory import UserFactory
from ..Models.User import User
import bcrypt
import pymongo


class UserRepository:
    user_store: pymongo.collection.Collection = None

    def __init__(self, user_store: pymongo.collection.Collection):
        self.user_store = user_store

    def check_credentials(self, username: str, password: str) -> bool:
        user = self.find_user_by_username(username)
        if user is None:
            return False
        return bcrypt.checkpw(password.encode(), user.hashedPassword.encode())

    def find_user_by_username(self, username: str):
        user = self.user_store.find_one({"username": username})
        return UserFactory.parse_user_dict(user) if user is not None else None

    def find_user_by_email(self, email: str):
        user = self.user_store.find_one({"email": email})
        return UserFactory.parse_user_dict(user) if user is not None else None

    def add_user(self, user: User):
        return self.user_store.insert_one(vars(user))
