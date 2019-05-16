using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Reflection;


namespace BrickHouse.DBTools
{
    class MongoTypeConverter
    {
        internal static BsonDocument ConvertToBsonDocument<T>(T dtoToConvert)
        {
            BsonDocument bsonDoc = new BsonDocument();
            BsonElement be = null;
            BsonValue bv = null;
            foreach (PropertyInfo pi in dtoToConvert.GetType().GetProperties())
            {
                Object propValue = pi.GetValue(dtoToConvert);
                bv = GetBsonValue(propValue);
                be = new BsonElement(pi.Name, bv);
                bsonDoc.Add(be);
            }
            return bsonDoc;
        }

        internal static BsonValue GetBsonValue(Object objValue)
        {
            BsonValue bv = null;
            switch (Type.GetTypeCode(objValue.GetType()))
            {
                case (TypeCode.String):
                    bv = new BsonString(objValue.ToString());
                    break;
                case (TypeCode.Int32):
                    bv = new BsonInt32(Convert.ToInt32(objValue));
                    break;
                case (TypeCode.Int64):
                    bv = new BsonInt64(Convert.ToInt64(objValue));
                    break;
                case (TypeCode.Double):
                    bv = new BsonDouble(Convert.ToDouble(objValue));
                    break;
                case (TypeCode.DateTime):
                    bv = new BsonDateTime(Convert.ToDateTime(objValue));
                    break;
                default:
                    bv = new BsonString(objValue.ToString());
                    break;
            }

            return bv;
        }


        internal static T ConvertFromBsonDoc<T>(BsonDocument bsonDoc) where T : new()
        {
            T retVal = new T();
            foreach (BsonElement element in bsonDoc.Elements)
            {
                switch (element.Value.GetType().ToString().ToLower())
                {
                    case ("mongodb.bson.bsondatetime"):
                        DateTime dateTimeVal = element.Value.ToUniversalTime();
                        if (retVal.GetType().GetProperty(element.Name) != null)
                        {
                            retVal.GetType().GetProperty(element.Name).SetValue(retVal, dateTimeVal);
                        }
                        break;
                    case ("mongodb.bson.bsondouble"):
                        Double doubleVal = element.Value.ToDouble();
                        if (retVal.GetType().GetProperty(element.Name) != null)
                        {
                            retVal.GetType().GetProperty(element.Name).SetValue(retVal, doubleVal);
                        }
                        break;
                    case ("mongodb.bson.bsonint64"):
                        Int64 int64Val = element.Value.ToInt64();
                        if (retVal.GetType().GetProperty(element.Name) != null)
                        {
                            retVal.GetType().GetProperty(element.Name).SetValue(retVal, int64Val);
                        }
                        break;
                    case ("mongodb.bson.bsonint32"):
                        Int32 int32Val = element.Value.ToInt32();
                        if (retVal.GetType().GetProperty(element.Name) != null)
                        {
                            retVal.GetType().GetProperty(element.Name).SetValue(retVal, int32Val);
                        }
                        break;
                    case ("mongodb.bson.bsonstring"):
                        String strVal = element.Value.AsString;
                        if (retVal.GetType().GetProperty(element.Name) != null)
                        {
                            retVal.GetType().GetProperty(element.Name).SetValue(retVal, strVal);
                        }
                        break;
                    default:
                        //Do nothing. I don't care
                        // String strVal = element.Value.AsString;
                        //retVal.GetType().GetProperty(element.Name).SetValue(retVal, strVal);
                        break;
                }
            }

            return retVal;
        }
    }
}
