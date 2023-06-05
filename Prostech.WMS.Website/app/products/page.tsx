"use client";
import Button from "@mui/material/Button";
import * as React from "react";
import ProductListTable from "../src/components/Table/ProductListTable";

const Products = () => {
  return (
    <main className="flex flex-col min-h-screen p-5 bg-gray-200">
      <div className="flex flex-row justify-between">
        <div className="text-lg font-bold text-black ">Products List</div>
        <Button variant="contained" color="success">
          + Add Product
        </Button>
      </div>
      <div className="min-h-screen bg-white">
        <div className="p-5 pt-10">
          <ProductListTable />
        </div>
      </div>
    </main>
  );
};

export default Products;
