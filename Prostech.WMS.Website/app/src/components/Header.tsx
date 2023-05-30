import Head from "next/head";
import Link from "next/link";
import { useState } from "react";
const Header = () => {
  return (
    <div>
      <nav className="w-full shadow bg-[#F4A4A4]">
        <div className="justify-between px-4 mx-auto lg:max-w-7xl md:items-center md:flex md:px-8">
          <div>
            <div className="flex items-center justify-between py-3 md:py-5 md:block">
              <a href="#">
                <h2 className="text-2xl font-bold text-white">
                  INVENTORY MANAGEMENT
                </h2>
              </a>
            </div>
          </div>
        </div>
      </nav>
    </div>
  );
};

export default Header;
