from ..Repositories.UserRepository import UserRepository


class RegisterInput:
    username: str = None
    password: str = None
    email: str = None

    def __init__(self, username: str, password: str, email: str):
        self.username = username
        self.password = password
        self.email = email


class RegisterPresenter:
    def __init__(self):
        pass

    def existing_email_error(self):
        pass

    def existing_username_error(self):
        pass

    def incorrect_password_format(self):
        pass


class RegisterUseCase:
    presenter: RegisterPresenter = None
    user_repo: UserRepository = None

    def __init__(self, presenter: RegisterPresenter):
        self.presenter = presenter

    def run(self, use_case_input: RegisterInput):
        pass
