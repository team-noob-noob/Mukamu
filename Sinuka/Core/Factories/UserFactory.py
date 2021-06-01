from ..Models.User import User
import bcrypt
import uuid

class UserFactory():
    def __init__(self):
        pass
    
    def CreateUser(self, username, password, email):
        id = uuid.uuid4()
        hashedPassword = bcrypt.hashpw(id, password.encode(), bcrypt.gensalt(7)).decode()
        return User(username, hashedPassword, email)
