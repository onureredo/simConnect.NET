import logo from '../assets/logo.png';

const Home = () => {
  return (
    <>
      <h1 className='text-4xl text-pink-700'>pilot's Life</h1>
      <img className='rounded-full w-40' src={logo} alt='' draggable='false' />
    </>
  );
};

export default Home;
