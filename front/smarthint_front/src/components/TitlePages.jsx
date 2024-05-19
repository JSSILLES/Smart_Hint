import React from "react";

export default function TitlePage({title, children}){
    return (
        <div className="d-flex align-items-end mt-2 pb-3 border-1 justify-content-center no-gutters">
            <h1 className='m-0 p-0'>{title}</h1>
            {children}
        </div>
    )
}