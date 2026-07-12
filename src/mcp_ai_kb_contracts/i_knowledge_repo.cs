using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace mcp_ai_kb_contracts {
      public interface i_knowledge_repo<T> {
            public void AddKnowledge(T knowledge);
            public void RemoveKnowledge(T knowledge);
            public T[] GetKnowledge();
            public T GetKnowledgeById(int id);

            public T GetKnowledgeByQuery(Expression<Func<T, bool>> predicate);
      }
}
