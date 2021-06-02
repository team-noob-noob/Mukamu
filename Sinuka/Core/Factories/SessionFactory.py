from ..Models.Session import Session
import uuid


class SessionFactory:
    def __init__(self):
        pass

    @staticmethod
    def create_session(user_id: uuid.UUID, token: str):
        id_ = uuid.uuid4()
        return Session
