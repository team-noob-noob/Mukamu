class User:
    id = ""
    username = ""
    hashedPassword = ""
    email = ""

    def __init__(self, id, username, password, email):
        self.id = id
        self.username = username
        self.hashedPassword = password
        self.email = email
