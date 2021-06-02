class Session:
    id_ = ""
    user_id = ""
    token = ""

    def __init__(self, id_, user_id, token):
        self.id_ = id_
        self.user_id = user_id
        self.token = token
