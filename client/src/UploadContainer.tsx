import { Row, Col, Alert } from "antd";
import { api } from "./api";
import { useState } from "react";
import { InfoCircleOutlined } from "@ant-design/icons";

interface Props {
  setIsLoading: (isLoading: boolean) => void;
}

export const UploadContainer = ({ setIsLoading }: Props) => {
  const [isDragging, setIsDragging] = useState(false);

  const onDragOver = (e: React.DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    setIsDragging(true);
  };

  const onDragEnd = (e: React.DragEvent<HTMLDivElement>) => {
    e.dataTransfer.clearData();
    setIsDragging(false);
  };

  const onDragLeave = (e: React.DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    setIsDragging(false);
  };

  const onDrop = async (e: React.DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    setIsLoading(true);
    setIsDragging(false);
    await api.uploadFiles(e.dataTransfer.files);
    setIsLoading(false);
  };

  return (
    <Row justify="center">
      <Col xs={24} md={8}>
        <div
          onDragOver={onDragOver}
          onDragEnd={onDragEnd}
          onDragLeave={onDragLeave}
          onDrop={onDrop}
        >
          <Alert
            showIcon
            icon={<InfoCircleOutlined />}
            message={"Ladda upp filer här"}
            type={isDragging ? "success" : "info"}
          ></Alert>
        </div>
      </Col>
    </Row>
  );
};
