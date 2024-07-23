export default {
    nodes: [
        {
            workflowId: '1',
            flag: 'Start',
            title: '发起人',
            people: null,
            signMethod: 'Na',
            passRate: 0,
            nextStep: [
                {
                    workflowNodeId: '1',
                    condition: {
                        value: null, function: 'x.amount >= 1500000000000000000000000'
                    },
                    nextNodeTitle: '总经理确认'
                },
                {
                    workflowNodeId: '1',
                    condition: {
                        value: null, function: 'x.amount >= 50000'
                    },
                    nextNodeTitle: '经理确认'
                },
                {
                    workflowNodeId: '1', condition: null, nextNodeTitle: '结束'
                }
            ],
            id: '1'
        },
        {
            workflowId: '1',
            flag: 'Process',
            title: '经理确认',
            people: {
                userId: [
                    {
                        id: '11', title: '经理'
                    }
                ],
                roleId: null,
                leaderLevel: null,
                fromDataField: null
            },
            signMethod: 'And',
            passRate: 0,
            nextStep: [
                {
                    workflowNodeId: '1',
                    condition: {
                        value: null, function: 'x.grossmargin <= 3.0'
                    },
                    nextNodeTitle: '总经理确认'
                },
                {
                    workflowNodeId: '1', condition: null, nextNodeTitle: '结束'
                }
            ],
            id: '2'
        },
        {
            workflowId: '1',
            flag: 'Process',
            title: '总经理确认',
            people: {
                userId: [
                    {
                        id: '11', title: '经理'
                    }
                ],
                roleId: null,
                leaderLevel: null,
                fromDataField: null
            },
            signMethod: 'And',
            passRate: 0,
            nextStep: [
                {
                    workflowNodeId: '1', condition: null, nextNodeTitle: '结束'
                }
            ],
            id: '2'
        },
        {
            workflowId: '1',
            flag: 'End',
            title: '结束',
            people: null,
            signMethod: 'Na',
            passRate: 0,
            nextStep: null,
            id: '3'
        }
    ],
    isDeleted: false,
    deleter: null,
    deletionTime: null,
    lastModificationTime: null,
    lastModifier: null,
    creationTime: '2024-07-22T14: 39: 46.53484+08: 00',
    creator: null,
    concurrencyStamp: '638572271865349673',
    id: '1',
    lazyLoader: {},
    title: '报价审核',
    catalog: 'EA'
}