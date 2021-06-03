from ..Repositories.UserRepository import UserRepository
from ..Factories.UserFactory import UserFactory


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

    def user_created(self):
        pass


class RegisterUseCase:
    presenter: RegisterPresenter = None
    user_repo: UserRepository = None
    user_factory: UserFactory = None

    def __init__(self, user_repo: UserRepository, user_factory: UserFactory):
        self.user_repo = user_repo
        self.user_factory = user_factory

    def run(self, use_case_input: RegisterInput):
        if self.user_repo.find_user_by_username(use_case_input.username) is not None:
            self.presenter.existing_username_error()
            return

        if self.user_repo.find_user_by_email(use_case_input.email) is not None:
            self.presenter.existing_email_error()
            return

        user = self.user_factory.create_user(
            use_case_input.username,
            use_case_input.password,
            use_case_input.email
        )

        self.user_repo.add_user(user)

        self.presenter.user_created()

    def set_presenter(self, presenter: RegisterPresenter):
        self.presenter = presenter
