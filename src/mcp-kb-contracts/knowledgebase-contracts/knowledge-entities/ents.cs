using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;


namespace knowledgebase_contracts.knowledge_entities {
      public static class ents {
            public enum kb_domain_type : byte {
                  none = 0,
                  mandate = 1,
                  adr = 2,
                  instruction = 3,
                  skill = 4,
                  index = 5,
                  knowledge_artifact = 6,
            }


            public enum kb_hash_algorithm_type : byte {
                  sha256 = 0
            }

            /// <summary>
            /// All registered modules must have a status entry in the 
            /// knowledgebase registry. This is a record that represents
            /// a module's last or current status entry.
            /// </summary>
            public sealed record status_entry(
                 int registry_entry_id,
                 DateTime initalized_utc,
                 DateTime last_updated_utc,
                 DateTime last_operation_utc,
                 bool is_active,
                 bool is_deprecated,
                 bool last_operation_successful
            );

            /// <summary>
            /// any module that operates within the domain of knowledgebase must register itself with the knowledgebase registry. 
            /// This is a record that represents a module's registration entry.
            /// </summary>
            public sealed record registry_entry(
                  kb_hash_algorithm_type hash_type,
                  kb_domain_type domain_type,
                  int id,
                  string name,
                  string path,
                  decimal version,
                  byte[ ] module_hash,
                  string long_description,
                  string short_description,
                  string[ ] keywords

            );

            /// <summary>
            /// Represents an entry for an inventory asset in storage. This record is used to store information about assets that are 
            /// part of the inventory, including their unique identifiers, names,
            /// and associated metadata.
            /// </summary>
            public sealed record inventory_asset_entry(

                );


            public sealed record mandate_entry(

                );


            public sealed record instruction_asset_entry(
                int id,
                string name,
                byte[ ] hash
                );



            public sealed record skill_asset_entry(
                int id,
                string name,
                byte[ ] hash,
    
                );



            public sealed record index_asset_entry(
                int id,
                string name,
                byte[ ] hash,
                string domain_storage_type,
                HashSet<string> keywords,
                int byte_size,
                int line_count,
                int word_count,
                int estimated_tokens
                );


            public sealed record knowledge_artifact_entry(
                int id,
                string name,
                byte[ ] hash,
                string content_body
                );






      }
}
