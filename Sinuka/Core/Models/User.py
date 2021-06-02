class User:
    id_ = ""
    username = ""
    hashedPassword = ""
    email = ""

    def __init__(self, id_, username, password, email):
        self.id_ = id_
        self.username = username
        self.hashedPassword = password
        self.email = email
