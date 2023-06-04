import React from "react";
import Chart from "react-apexcharts";
function PieChart() {
  return (
    <React.Fragment>
      <div className="container-fluid">
        <h3>welcome to pie chart</h3>
        <Chart
          type="pie"
          width={500}
          options={{
            labels: ["a", "b", "v", "f"],
          }}
          series={[10, 23, 40, 50]}
        ></Chart>
      </div>
    </React.Fragment>
  );
}
export default PieChart;
