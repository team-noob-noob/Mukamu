import pymongo

Client = pymongo.MongoClient("mongodb://localhost:27017")

Database = Client["Sinukaka"]
