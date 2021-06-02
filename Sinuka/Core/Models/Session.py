import uuid


class Session:
    id_: uuid.UUID = ""
    user_id: uuid.UUID = ""
    token: str = ""

    def __init__(self, id_: uuid.UUID, user_id: uuid.UUID, token: str):
        self.id_ = id_
        self.user_id = user_id
        self.token = token
