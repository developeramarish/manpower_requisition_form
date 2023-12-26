// CardComponent.js
import React from 'react';
import { Card } from 'primereact/card';

const CardComponent = ({ title, content }) => {
  return (
    <Card title={title}>
      <p>{content}</p>
    </Card>
  );
};

export default CardComponent;
