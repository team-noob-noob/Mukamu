from ..Models.User import User
import bcrypt
import uuid


class UserFactory:
    def __init__(self):
        pass

    @staticmethod
    def create_user(username, password, email):
        id_ = uuid.uuid4()
        hashed_password = bcrypt.hashpw(password.encode(), bcrypt.gensalt(7)).decode()
        return User(id_, username, hashed_password, email)
