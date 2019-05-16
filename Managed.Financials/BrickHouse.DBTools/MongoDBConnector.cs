using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace BrickHouse.DBTools
{
    //public class MongoDBConnector : IDBConnector
    //{
    //    MongoServer _server;
    //    MongoDatabase _database;
    //    MongoDB.Driver.MongoClient _client;
    //    public MongoDBConnector()
    //    {
    //        _client = new MongoClient();
    //        _server = _client.GetServer();
    //        _database = _server.GetDatabase("test");
    //    }

    //    public void Insert<T>(List<T> objects)
    //    {
    //        if (!(objects.Count > 0))
    //        {
    //            return;
    //        }
    //        String objectName = GetTypeName<T>(objects);
    //        MongoCollection<BsonDocument> objEntries = _database.GetCollection<BsonDocument>(objectName);
    //        BsonDocument doc = null;
    //        foreach (T obj in objects)
    //        {
    //            doc = MongoTypeConverter.ConvertToBsonDocument<T>(obj);
    //            objEntries.Insert(doc);
    //        }
    //    }

    //    private String GetTypeName<T>(List<T> objects)
    //    {
    //        return objects[0].GetType().Name;
    //    }

    //    public IQueryResult<T> Query<T, ConditionType>(string objectName, Conditions.QueryCondition<ConditionType> condition) where T : new()
    //    {

    //        IQueryResult<T> retVal = new QueryResult<T>();
    //        List<T> results = new List<T>();
    //        retVal.Success = true;
    //        retVal.Message = String.Empty;
    //        //run some shitty query
    //        try
    //        {
    //            var collection = _database.GetCollection(objectName);

    //            BsonElement queryElement = TranslateCondition<ConditionType>(condition);
    //            var query = new QueryDocument() { queryElement };

    //            var mCursor = collection.Find(query);
    //            T convertedVal;
    //            foreach (var whatever in mCursor)
    //            {
    //                convertedVal = MongoTypeConverter.ConvertFromBsonDoc<T>(whatever);
    //                results.Add(convertedVal);
    //            }
    //            retVal.Message = String.Format("Returned {0} results of {1}", results.Count, objectName);
    //        }
    //        catch (System.Exception ex)
    //        {
    //            retVal.Success = false;
    //            retVal.Message = ex.ToString();
    //        }

    //        retVal.Results = results;
    //        return retVal;

    //    }


    //    public void Delete<T>(List<T> itemsToDelete)
    //    {
    //        if (!(itemsToDelete.Count > 0))
    //        {
    //            return;
    //        }
    //        String objectName = GetTypeName<T>(itemsToDelete);
    //        SelectQuery deleteQuery = new SelectQuery(_database.GetCollection(objectName), itemsToDelete[0].GetType());
    //        // MongoDB.Driver.Builders.Query.In()
    //        //need to create a IMongoQuery to delete the values

    //    }


    //    private static BsonElement TranslateCondition<ConditionType>(Conditions.QueryCondition<ConditionType> condition)
    //    {
    //        String fieldName = condition.FieldName;
    //        BsonElement retVal = null;
    //        BsonValue bVal = null;
    //        Object fieldValue = condition.Value;

    //        bVal = MongoTypeConverter.GetBsonValue(fieldValue);

    //        retVal = new BsonElement(fieldName, bVal);
    //        return retVal;
    //    }
    //}
}
