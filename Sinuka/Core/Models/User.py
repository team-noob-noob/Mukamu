import bcrypt

class User:
    username = ""
    hashedPassword = ""
    email = ""

    def __init__(self, username, password, email):
        self.username = username
        self.hashedPassword = password
        self.email = email

    # UserRepository
    def CheckPassword(self, password):
        return bcrypt.checkpw(password.encode(), self.hashedPassword.encode())

