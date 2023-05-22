/* Autogenerated file. Manual edits will not be saved.*/

#nullable enable
using System;
using mud.Client;
using mud.Network.schemas;
using mud.Unity;
using UniRx;
using Property = System.Collections.Generic.Dictionary<string, object>;

namespace DefaultNamespace
{
    public class HealthTableUpdate : TypedRecordUpdate<Tuple<HealthTable?, HealthTable?>> { }

    public class HealthTable : IMudTable
    {
        public static readonly TableId TableId = new("", "Health");

        public ulong? value;

        public static HealthTable? GetTableValue(string key)
        {
            var query = new Query()
                .Find("?value", "?attribute")
                .Where(TableId.ToString(), key, "?attribute", "?value");
            var result = NetworkManager.Instance.ds.Query(query);
            var healthTable = new HealthTable();
            var hasValues = false;

            foreach (var record in result)
            {
                var attribute = record["attribute"].ToString();
                var value = record["value"];

                switch (attribute)
                {
                    case "value":
                        var valueValue = (ulong)value;
                        healthTable.value = valueValue;
                        hasValues = true;
                        break;
                }
            }

            return hasValues ? healthTable : null;
        }

        public static IObservable<HealthTableUpdate> OnRecordUpdate()
        {
            return NetworkManager.Instance.ds.OnDataStoreUpdate
                .Where(
                    update =>
                        update.TableId == TableId.ToString() && update.Type == UpdateType.SetField
                )
                .Select(
                    update =>
                        new HealthTableUpdate
                        {
                            TableId = update.TableId,
                            Key = update.Key,
                            Value = update.Value,
                            TypedValue = MapUpdates(update.Value)
                        }
                );
        }

        public static IObservable<HealthTableUpdate> OnRecordInsert()
        {
            return NetworkManager.Instance.ds.OnDataStoreUpdate
                .Where(
                    update =>
                        update.TableId == TableId.ToString() && update.Type == UpdateType.SetRecord
                )
                .Select(
                    update =>
                        new HealthTableUpdate
                        {
                            TableId = update.TableId,
                            Key = update.Key,
                            Value = update.Value,
                            TypedValue = MapUpdates(update.Value)
                        }
                );
        }

        public static IObservable<HealthTableUpdate> OnRecordDelete()
        {
            return NetworkManager.Instance.ds.OnDataStoreUpdate
                .Where(
                    update =>
                        update.TableId == TableId.ToString()
                        && update.Type == UpdateType.DeleteRecord
                )
                .Select(
                    update =>
                        new HealthTableUpdate
                        {
                            TableId = update.TableId,
                            Key = update.Key,
                            Value = update.Value,
                            TypedValue = MapUpdates(update.Value)
                        }
                );
        }

        public static Tuple<HealthTable?, HealthTable?> MapUpdates(
            Tuple<Property?, Property?> value
        )
        {
            HealthTable? current = null;
            HealthTable? previous = null;

            if (value.Item1 != null)
            {
                try
                {
                    current = new HealthTable
                    {
                        value = value.Item1.TryGetValue("value", out var valueVal)
                            ? (ulong)valueVal
                            : default,
                    };
                }
                catch (InvalidCastException)
                {
                    current = new HealthTable { value = null, };
                }
            }

            if (value.Item2 != null)
            {
                try
                {
                    previous = new HealthTable
                    {
                        value = value.Item2.TryGetValue("value", out var valueVal)
                            ? (ulong)valueVal
                            : default,
                    };
                }
                catch (InvalidCastException)
                {
                    previous = new HealthTable { value = null, };
                }
            }

            return new Tuple<HealthTable?, HealthTable?>(current, previous);
        }
    }
}
