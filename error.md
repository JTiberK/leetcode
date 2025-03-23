
### USER
Parece que este código JavaScript da error:

```
Runtime Error
/.nvm/versions/node/v22.14.0/lib/node_modules/@datastructures-js/priority-queue/src/minPriorityQueue.js:20
      throw new Error('MinPriorityQueue constructor requires a callback for object values');
      ^
Error: MinPriorityQueue constructor requires a callback for object values
    at new MinPriorityQueue (/.nvm/versions/node/v22.14.0/lib/node_modules/@datastructures-js/priority-queue/src/minPriorityQueue.js:20:13)
    Line 23: Char 14 in solution.js (countPaths)
    Line 63: Char 19 in solution.js (Object.<anonymous>)
    Line 16: Char 8 in runner.js (Object.runner)
    Line 50: Char 26 in solution.js (Object.<anonymous>)
    at Module._compile (node:internal/modules/cjs/loader:1554:14)
    at Object..js (node:internal/modules/cjs/loader:1706:10)
    at Module.load (node:internal/modules/cjs/loader:1289:32)
    at Function._load (node:internal/modules/cjs/loader:1108:12)
    at TracingChannel.traceSync (node:diagnostics_channel:322:14)
Node.js v22.14.0
View less
Last Executed Input
Open Testcase
n =
7
roads =
[[0,6,7],[0,1,2],[1,2,3],[1,3,3],[6,3,3],[3,5,1],[6,5,1],[2,5,1],[0,4,5],[4,6,2]]
```
