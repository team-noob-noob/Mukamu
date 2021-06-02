def main():
    from Core.Factories.UserFactory import UserFactory
    from Core.Repositories.UserRepository import UserRepository
    from Core.Database.Collections import Users

    user = UserFactory.create_user("test", "test", "test")

    repo = UserRepository(Users)
    repo.add_user(user)

    fetched_user = repo.find_user_by_username("test")
    print(fetched_user.id_)
    print(fetched_user.username)
    print(fetched_user.email)
    print(fetched_user.hashed_password)


if __name__ == "__main__":
    main()
