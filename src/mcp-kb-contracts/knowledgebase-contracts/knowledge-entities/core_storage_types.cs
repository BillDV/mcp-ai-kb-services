using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace knowledgebase_contracts.knowledge_entities
{
    public static class core_storage_types
    {


        public sealed record domain_status(
            
            );


        public sealed record domain_registry_entry(
            
            );

        public sealed record domain_inventory_entry(
            
            );


        public sealed record domain_mandate(
            
            );


        public sealed record domain_instruction(
            
            );



        public sealed record domain_skill(
            
            );

        public sealed record domain_index(
            int id,
            string name,
            byte[] hash,
            string domain_storage_type,
            HashSet<string> keywords,
            int bytesize,
            int lines,
            int estimated_tokens
            );









    }
}
