class User:
    username = ""
    hashedPassword = ""
    email = ""

    def __init__(self, id, username, password, email):
        self.username = username
        self.hashedPassword = password
        self.email = email
