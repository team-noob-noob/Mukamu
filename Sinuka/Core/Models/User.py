class User:
    id_ = ""
    username = ""
    hashed_password = ""
    email = ""

    def __init__(self, id_, username, password, email):
        self.id_ = id_
        self.username = username
        self.hashed_password = password
        self.email = email
