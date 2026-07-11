using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;


namespace knowledgebase_contracts.knowledge_entities {
      public static partial class ents {

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
                  byte[] module_signature,
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

        /// <summary>
        /// adr's control every aspect of the knowledgebase. They are the highest level of control; used to define the rules and regulations that govern the knowledgebase.
        /// adr's are "packaged" as mandates once approved, which are then registered with the knowledgebase registry . 
        /// The mandate is the highest level of control and is used to define the rules and regulations that govern the knowledgebase.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="hash"></param>
        /// <param name="content_body"></param>
        public sealed record adr_asset_entry(
                int id,
                string name,
                byte[] hash,
                string content_body
                );

        /// <summary>
        /// instructions provide guidance on how things should be implemented and styled.
        /// ADR -> Mandate -> Instruction -> Skill -> Index -> Knowledge Artifact <- Agent
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="hash"></param>
        /// <param name="content_body"></param>
        public sealed record instruction_asset_entry(
                       int id,
                string name,
                byte[] hash,
                string content_body
                );



            public sealed record skill_asset_entry(
                         int id,
                string name,
                byte[] hash,
                string content_body
                );


            /// <summary>
            /// 
            /// </summary>
            /// <param name="id"></param>
            /// <param name="name"></param>
            /// <param name="hash"></param>
            /// <param name="domain_storage_type"></param>
            /// <param name="keywords"></param>
            /// <param name="byte_size"></param>
            /// <param name="line_count"></param>
            /// <param name="word_count"></param>
            /// <param name="estimated_tokens"></param>
            public sealed record index_entry(
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
