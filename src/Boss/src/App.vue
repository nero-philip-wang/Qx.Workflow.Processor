<script setup>
import WorkflowNode from './components/WorkflowNode.vue'
const root = { nextNodeTitle: '发起人' }
</script>

<template>
  <WorkflowNode :node="root" />
</template>

<style scoped>
</style>
