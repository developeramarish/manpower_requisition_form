import React from 'react'
import Dashboard from './Dashboard'

const PageContainer = ({Comp}) => {
    console.log(Comp)
    return (
        <div>
            <Comp />
        </div>

    )
}

export default PageContainer;