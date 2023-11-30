import React, { useState } from 'react';
import { Button } from 'primereact/button';
import { OverlayPanel } from 'primereact/overlaypanel';
import { Dialog } from 'primereact/dialog';

const PopupDemo = ({ visible, onHide }) => {
  
    return (
        <div>

           < Dialog  
    visible={visible} onHide={onHide}
    className='p-dialog-titlebar-close'
     >
     <h3>  This MRF is yet to be resubmitted</h3> 
    </Dialog> 
    
        </div>
    );
};
export default PopupDemo;
