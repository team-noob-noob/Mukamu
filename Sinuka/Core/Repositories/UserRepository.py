from ..Database.Collections import Users
import bcrypt

class UserRepository():
    def __init__(self):
        pass

    def CheckCredentials(self, username, password):
        user = self.FindUserByUsername(username)
        return bcrypt.checkpw(password.encode(), user.hashedPassword.encode())

    def FindUserByUsername(self, username):
        return Users.find_one({"username": username})

    def AddUser(self, user):
        Users.insert_one(vars(user))
