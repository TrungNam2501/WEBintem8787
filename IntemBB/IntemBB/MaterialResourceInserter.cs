using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json.Serialization;
using IntemBB;
using Newtonsoft.Json;
using Npgsql;

public class MaterialResourceInserter
{
    public static bool InsertMaterialResource(  string idVal, string productId, string productType,
        decimal quantity, int status, long expiryTime, long updatedAt, string updatedBy,
        long createdAt, string createdBy,string station, long standingTime)
    {
        string productionTime = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");

        Guid oid = Guid.NewGuid();
        var infoObject = new
        {
            unit = "",
            grade = "",
            remark = "",
            change_log = new List<object>(),
            lot_number = "",
            min_dosage = "0",
            hold_reason = 0,
            inspections = new List<object>(),
            deferrals_count = 0,
            production_info = new
            {
                station = "",
                recipe_id = "",
                next_station = "",
                process_name = "",
                process_type = "",
                production_time = productionTime
            },
            planned_quantity = "0",
            additional_fields = (object)null
        };

        string infoJson = JsonConvert.SerializeObject(infoObject);

        string insertSql = @"
            INSERT INTO kvmes.material_resource (
                oid, id, product_id, product_type, quantity, status,
                expiry_time, info, warehouse_id, warehouse_location,
                updated_at, updated_by, created_at, created_by,
                station, feed_records_id, batch_count, reprint_reason,
                collected, erp_tire_barcode_synced, standing_time
            ) VALUES (
                @oid, @id, @product_id, @product_type, @quantity, @status,
                @expiry_time, @info::jsonb, @warehouse_id, @warehouse_location,
                @updated_at, @updated_by, @created_at, @created_by,
                @station, @feed_records_id, @batch_count, @reprint_reason,
                @collected, @erp_tire_barcode_synced, @standing_time
            )
            ON CONFLICT (id, product_id) DO NOTHING;
        ";

        var parameters = new Dictionary<string, object>
        {
            { "oid", oid },
            { "id", idVal },
            { "product_id", productId },
            { "product_type", productType },
            { "quantity", quantity },
            { "status", status },
            { "expiry_time", expiryTime },
            { "info", infoJson },
            { "warehouse_id", "" },
            { "warehouse_location", "" },
            { "updated_at", updatedAt },
            { "updated_by", updatedBy },
            { "created_at", createdAt },
            { "created_by", createdBy },
            { "station", station },
            { "feed_records_id", Array.Empty<Guid>() }, 
            { "batch_count", 0 },
            { "reprint_reason", 0 },
            { "collected", false },
            { "erp_tire_barcode_synced", false },
            { "standing_time", standingTime }
        };
        return SQLproPg.ExecuteNonQueryPg(insertSql, CommandType.Text, parameters);
    }
}
