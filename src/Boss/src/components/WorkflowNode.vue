<script lang="jsx">
import { reactive } from 'vue'
import json from './data.js'

function appendNode(step) {
  if (!step.nodes) step.node = json.nodes.filter((c) => c.title == step.nextNodeTitle)[0]
  return step
}
function getUser(people) {
  if (people?.userId) {
    return people.userId.map((c) => c.title).join(',')
  }
}
function getColor(node) {
  switch (node?.flag) {
    case 'Start': return 'bg-emerald-300';
    case 'Process': return 'bg-sky-600';
    case 'End': return 'bg-blue-300';
  }
}



export default {
  props: {
    node: {
      type: Object,
      required: true
    }
  },

  setup(props) {
    var render = (nodes) => (nodes ?
      <div class="flex justify-center text-center mb-4 text-xs">
        {nodes.map((c, i) =>
          <div class={`flex flex-col items-center gap-y-4 border-sky-500 border-dashed ${nodes.length > 1 && i > 0 && 'border-l'}`}  >
            <div class="cursor-pointer">
              <div class="condition truncate w-40 px-2 mx-2 text-sky-500 h-4">{c.condition?.function || ''}</div>
              <div class={`border rounded-md p-2 text-white mx-2 shadow w-40 h-18 ${getColor(c.node)}`}>
                <div class="title border-b border-white p-1 truncate">
                  {c.nextNodeTitle} {c.node.signMethod}
                </div>
                <div class="reviewer p-1 truncate ">{getUser(c.node.people) || c.nextNodeTitle}</div>
              </div>
            </div>
            <div class="  ">{render(c.node?.nextStep?.map(d => appendNode(d)))}</div>
          </div>
        )}
      </div> : null
    );

    return () => render([reactive(appendNode(props.node))])
  }
}
</script>