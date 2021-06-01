from ..Models.User import User
import bcrypt

class UserFactory():
    def __init__(self):
        pass
    
    def CreateUser(self, username, password, email):
        hashedPassword = bcrypt.hashpw(password.encode(), bcrypt.gensalt(7)).decode()
        return User(username, hashedPassword, email)
